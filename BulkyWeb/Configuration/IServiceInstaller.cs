﻿namespace BulkyWeb.Configuration;

public interface IServiceInstaller
{
	void Install(IServiceCollection services, IConfiguration configuration);
}
