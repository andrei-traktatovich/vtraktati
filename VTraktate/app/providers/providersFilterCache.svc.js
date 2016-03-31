(() => {
    "use strict";
    angular.module("providers")
        .factory("providersFilterCache", (LocalStorageService, notifyClient) => {
            
            const MYFILTERSETTINGS_KEY = "my_provider_filter_settings";

            return {
                save,
                load,
                clear
            };
            

            function save(settings) {
                LocalStorageService.set(MYFILTERSETTINGS_KEY, settings);
                notifyClient.info("Настройки фильтра сохранены.");
            }

            function load() {
                var settings = LocalStorageService.get(MYFILTERSETTINGS_KEY) || {};
                notifyClient.info("Настройки фильтра восстановлены.");
                return settings;
            }

            function clear() {
                LocalStorageService.remove(MYFILTERSETTINGS_KEY);
            }
        });

})();