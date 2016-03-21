(function () {
    
    function User() {
        
        var userData = null,
            currentOfficeId = null;

        return {
            set: set,
            get: function (arg) { return typeof arg === 'string' ? userData[arg] : userData; },
            currentOfficeId: getsetCurrentOfficeId,
            clear: clear
        };

        function getsetCurrentOfficeId(id) {
            if (angular.isNumber(id)) {
                if (id < 1)
                    throw new Error('office codes should be a positive number');
                currentOfficeId = id;
            }
            return currentOfficeId;
        }
        function set(value) {
            if (value) {
                console.log('setting user');
                userData = value;
                currentOfficeId = userData.officeId;
                console.log(userData);
            }
            else {
                throw new Error('User: ошибка - попытка присвоить пустое значение user');
            }
        }

        function clear() {
            userData = null;
        }
    }
    
    angular.module('user', [])
        .value('User', new User());
})();