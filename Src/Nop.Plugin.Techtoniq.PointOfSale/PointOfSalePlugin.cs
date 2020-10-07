using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Techtoniq.PointOfSale
{
    public class PointOfSalePlugin : BasePlugin, IMiscPlugin
    {
        #region Fields

        private readonly IWebHelper _webHelper;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;

        #endregion Fields

        #region Ctor

        public PointOfSalePlugin(IWebHelper webHelper, ILocalizationService localizationService, ICustomerService customerService)
        {
            _webHelper = webHelper;
            _customerService = customerService;
            _localizationService = localizationService;
        }

        #endregion Ctor

        #region Methods

        public override void Install()
        {
            base.Install();

            _localizationService.AddOrUpdatePluginLocaleResource("TQPOS.AdminLink", "Point of Sale");
            EnsurePOSRoleExistsAndIsActive();
        }

        public override void Uninstall()
        {
            _localizationService.DeletePluginLocaleResource("TQPOS.AdminLink");
            RemovePOSRole();

            base.Uninstall();
        }
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/PointOfSale/Configure";
        }

        #endregion Methods

        #region Internal Methods

        private void EnsurePOSRoleExistsAndIsActive()
        {
            var posRole = _customerService.GetCustomerRoleBySystemName(Domain.Constants.Roles.POSRoleSystemName);

            if (null == posRole)
            {
                posRole = new CustomerRole
                {
                    Name = Domain.Constants.Roles.POSRoleName,
                    Active = true,
                    SystemName = Domain.Constants.Roles.POSRoleSystemName
                };

                _customerService.InsertCustomerRole(posRole);
            }
            else if (!posRole.Active)
            {
                posRole.Active = true;
                _customerService.UpdateCustomerRole(posRole);
            }
        }

        private void RemovePOSRole()
        {
            var posRole = _customerService.GetCustomerRoleBySystemName(Domain.Constants.Roles.POSRoleSystemName);
            if (null != posRole)
            {
                posRole.Active = false;
                _customerService.UpdateCustomerRole(posRole);
            }
        }

        #endregion Internal Methods
    }
}
