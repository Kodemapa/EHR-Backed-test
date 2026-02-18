using System.Linq;
using System.Threading;
using EHR_Application.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EHR_Application.Interceptors;

public sealed class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<AuditSaveChangesInterceptor> _logger;

    public AuditSaveChangesInterceptor(ICurrentUserService currentUserService, ILogger<AuditSaveChangesInterceptor> logger)
    {
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        LogPendingChanges(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        LogPendingChanges(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private void LogPendingChanges(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        var user = _currentUserService.UserName ?? _currentUserService.UserId ?? "anonymous";
        foreach (var entry in context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged && e.State != EntityState.Detached))
        {
            _logger.LogInformation("User {User} is {State} entity {Entity}", user, entry.State, entry.Entity.GetType().Name);
        }
    }
}
