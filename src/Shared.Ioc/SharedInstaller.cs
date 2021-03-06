﻿using System.Reflection.Metadata.Ecma335;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using AutoMapper;
using DataAccess.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Service.Implementation;
using Microsoft.Extensions.Configuration;

namespace Shared.Ioc
{
    public static class SharedInstaller
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = services.BuildServiceProvider().GetService<IOptions<Settings>>().Value;
            services.AddTransient<IPatientsService, PatientsService>();
            services.AddTransient<IStorageHandler, StorageHandler>();
            services.AddDbContext<PatientsDbContext>(options =>
                options.UseSqlServer(appSettings.DataAccessSettings.ConnectionString));
            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper());
            services.AddSingleton<AmazonS3Client>(x =>
            {
                var client = new AmazonS3Client(
                    new BasicAWSCredentials(appSettings.StorageSettings.AccessKey,
                        appSettings.StorageSettings.AccessKeyId),
                    new AmazonS3Config()
                    {
                        ServiceURL = appSettings.StorageSettings.ServiceURL,
                        ForcePathStyle = true,
                    });
                return client;
            });
        }
    }
}
