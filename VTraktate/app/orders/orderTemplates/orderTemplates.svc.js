
(function () {
    angular.module('orderTemplate')
        .service('orderTemplates', orderTemplates);
        
    function orderTemplates(LocalStorageService) {
        var ls = LocalStorageService,
            LS_KEY = 'order_templates';

        return {
            get: get,
            clear: clear,
            set: set
        }
        function getLSContent() {
            return LocalStorageService.get(LS_KEY) || [];
        }
        function setLSContent(value) {
            LocalStorageService.set(LS_KEY, value);
        }

        function get(templateName) {
            var lsContent = getLSContent();
            console.log('ls content=');
            console.log(lsContent);

            if (!templateName)
                return lsContent;
            else {
                var item = _.findWhere(lsContent, { name: templateName });
                console.log('templateitem retrieved')
                console.log(item);
                return item;
            }
        }

        function clear(templateName) {
            var lsContent = getLSContent();
            if (!templateName)
                lsContent = {};
            else
                delete lsContent[templateName];
            setLSContent(lsContent);
        }

        function set(templateName, template) {
            var lsContent = getLSContent();
            if (!templateName)
                throw new Error('order template name empty');
            var item = _.findWhere(lsContent, { name: templateName });
            if (item)
                item.template = template;
            else
                lsContent.push({ name: templateName, template: template });
            setLSContent(lsContent);
        }
    }
})();