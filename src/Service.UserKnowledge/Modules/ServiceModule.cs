﻿using Autofac;
using Service.ServerKeyValue.Client;

namespace Service.UserKnowledge.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterKeyValueClient(Program.Settings.ServerKeyValueServiceUrl);
		}
	}
}