using System;
using System.Collections.Generic;
using System.Text;
using Oakell.Localization;
using Volo.Abp.Application.Services;

namespace Oakell;

/* Inherit your application services from this class.
 */
public abstract class OakellAppService : ApplicationService
{
    protected OakellAppService()
    {
        LocalizationResource = typeof(OakellResource);
    }
}
