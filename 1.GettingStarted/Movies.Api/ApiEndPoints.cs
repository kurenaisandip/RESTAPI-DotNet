﻿namespace Movies.Api;

public static class ApiEndPoints
{
 private const string ApiBase = "api";

 public static class Movies
 {
  private const string Base = $"{ApiBase}/movies";

  public const string Create = Base;
  
  public const string Get = $"{Base}/{{id:Guid}}";

  public const string GetAll = Base;
  
  public const string Update = $"{Base}/{{id:Guid}}";
  
  public const string Delete = $"{Base}/{{id:Guid}}";
 }

}