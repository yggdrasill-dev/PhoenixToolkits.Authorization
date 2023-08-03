using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Options;

namespace Valhalla.Authorization.AspNetCore;

public class HttpPathFeature : IHttpFeature
{
	private readonly TemplateMatcher m_Matcher;
	private readonly Dictionary<string, CompositeRouteConstraint> m_Constraints;

	public HttpPathFeature(
#if NET7_0_OR_GREATER
		[StringSyntax("Route")]
#endif
		string pathPattern,
		IServiceProvider serviceProvider)
	{
		m_Matcher = new TemplateMatcher(
			new RouteTemplate(RoutePatternFactory.Parse(pathPattern)),
			new RouteValueDictionary());

		var constraintResolver = new DefaultInlineConstraintResolver(
			new OptionsWrapper<RouteOptions>(new RouteOptions()),
			serviceProvider);

		m_Constraints = m_Matcher.Template.Parameters.ToDictionary(
			p => p.Name!,
			p => new CompositeRouteConstraint(p.InlineConstraints
				.Select(inline => constraintResolver.ResolveConstraint(inline.Constraint))
				.Where(c => c is not null)
				.Select(c => c!)))!;
	}

	public bool IsMatch(HttpContext httpContext)
	{
		var dict = new RouteValueDictionary();

		return m_Matcher.TryMatch(httpContext.Request.Path, dict)
			&& MatchConstraints(httpContext, dict);
	}

	private bool MatchConstraints(HttpContext ctx, RouteValueDictionary routeValues)
	{
		foreach (var kvp in m_Constraints)
		{
			var constraint = kvp.Value;

			if (!constraint.Match(
				ctx,
				null,
				kvp.Key,
				routeValues,
				RouteDirection.IncomingRequest))
				return false;
		}

		return true;
	}
}
