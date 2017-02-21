**v0.1.4.0 (2017-02-21)**
* NEW: StreamDecorator expose InnerStream
* NEW: AuthenticationResultDTO
* REFACTOR: UserManager.Signin return user info
* REFACTOR: BaseDbContext renamed to FrameworkDbContext

**v0.1.3.0 (2017-02-14)**
* RENAMED: IRepository.ListAll renamed to GetAll
* NEW: IRepository.GetBy
* NEW: BaseDbContext.CreateSessionScope
* REMOVED: FrameworkController.ExecuteAjax
* NEW: FrameworkController.ExecuteJsonResult

**v0.1.2.0 (2017-02-10)**
* REMOVED: IDbEngine.ExecuteCommand
* REMOVED: TextReplacer
* NEW: DbSriptExecuteOptions
* NEW: CommandLine
* FrameworkDatabase use lazy creation of sessionFactory

**v0.1.1.1 (2017-02-09)**
* Add -v parameter to DbManagementCommandOptions
* Add Find/FindIndex to CollectionUtil
* CollectionUTil.FindBy/FindByName marked as obsolute

**v0.1.1.0 (2017-02-09)**
* Add -v parameter to DbManagementCommandOptions
* Add Find/FindIndex to CollectionUtil
* CollectionUTil.FindBy/FindByName marked as obsolute

**v0.1.0.15 (2017-01-22)**
* FIXED: IQuery<T>.Set unified

**v0.1.0.14 (2017-01-22)**
* FIXED: Pager result set Disabled and Active
* FIXED: Pager always set text to number
* REFACTOR: Query and Query<T> return self
* NEW: QueryDTO.ToCritias

**v0.1.0.13 (2017-01-22)**
* NEW: Add ContentReplacer/WorkingDirectory parameter to FrameworkDatabase

**v0.1.0.12 (2017-01-21)**
* NEW: CritiaList
* REMOVED: RedirectData and FrameworkController.ExecuteAndRedirect(RedirectData)

**v0.1.0.11 (2017-01-19)**
* FIX: ExecuteAndRedirect set TempMessage
* NEW: ExecuteAndRedirect successMessage can be null
* NEW: ExecuteAndRedirect redirect param set to url
* NEW: MemberUtil

