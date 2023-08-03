﻿namespace Valhalla.Authorization.AspNetCore;

internal class HttpFunctionMatcher : IFunctionMatcher
{
	private readonly HttpContext m_Context;

	public HttpFunctionMatcher(HttpContext context)
	{
		m_Context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public bool IsMatch(IFunction function)
		=> function is HttpFunction httpFunction
			&& httpFunction.IsMatch(m_Context);
}
