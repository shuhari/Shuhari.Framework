TODO:
BaseDbContext renamed to FrameworkDbContext
EnumUtil.ToSelectItems()
StreamDecorator expose InnerStream

DONE:
* FIXED: IQuery<T>.Set unified
* Add -v parameter to DbManagementCommandOptions
* Add Find/FindIndex to CollectionUtil
* CollectionUTil.FindBy/FindByName marked as obsolute
* NEW: GZip.Compress/Decompress
* NEW: Encryption.SHA1
* REMOVED: CollectionUtil.FindBy/FindByName
* REMOVED: IDbEngine.ExecuteCommand
* REMOVED: TextReplacer
* NEW: DbSriptExecuteOptions
* NEW: CommandLine
* FrameworkDatabase use lazy creation of sessionFactory
* REFACTOR: FrameworkDatabase lazy create sessionFactory instead of startup
* REMOVED: DbEngine.ExecuteResourceScript/DbManagementCommandOptions
* RENAMED: IRepository.ListAll renamed to GetAll
* NEW: IRepository.GetBy
* NEW: BaseDbContext.CreateSessionScope
* REMOVED: FrameworkController.ExecuteAjax
* NEW: FrameworkController.ExecuteJsonResult
