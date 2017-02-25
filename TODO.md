TODO:
EnumUtil.ToSelectItems()

DONE:
* REFACTOR: FrameworkDatabase lazy create sessionFactory instead of startup
* REMOVED: DbEngine.ExecuteResourceScript/DbManagementCommandOptions
* RENAMED: IRepository.ListAll renamed to GetAll
* NEW: IRepository.GetBy
* NEW: BaseDbContext.CreateSessionScope
* REMOVED: FrameworkController.ExecuteAjax
* NEW: FrameworkController.ExecuteJsonResult
* NEW: StreamDecorator expose InnerStream
* NEW: AuthenticationResultDTO
* REFACTOR: UserManager.Signin return user info
* REFACTOR: BaseDbContext renamed to FrameworkDbContext
* REFACTOR: MappingFactory.MapEntitiesWithAnnonations set to public
* NEW: DbContextFactory
