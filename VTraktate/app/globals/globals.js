(function () {
    angular.module("globals", [ "thirdPartyServices" ])
    .factory("GlobalsService", globals);

    function globals($q, _) {

        var globalVariables = null,
            lists = {
                tripleChoice: [{ title: "-" }, { id: true, title: "Да" }, { id: false, title: "Нет" }]
            };

        return {
            set: set,
            get: get,
            getFilter: getFilter,
            getExcept: getExcept,
            clear: clear,
            getAndInsertAll: getAndInsertAll,
            populateLists: populateLists,
            getNameById: getNameById
        };
        
        function getNameById(collectionName, id) {
            console.log("getNameById: looking for collection " + collectionName);
            var arr = globalVariables[collectionName];
            if (!arr)
                throw Error(`getNameById: collection ${collectionName} does not exist!`);
            if (!arr.length)
                throw Error(`getNameById: collection ${ collectionName } has zero length!`)
            var item = _.find(arr, byId(id));
            return item ? item.name || item.title || "something" : "Item not found";
        }

        function byId(id) {
            return (element) => element.id === id;
        }

        function set(value) {
            if (value) {
                globalVariables = value;
                globalVariables.tripleChoice = angular.copy(lists.tripleChoice);
            }
            else
                throw new Error("GlobalsService: попытка присвоить пустое значение globals");
        }
        function getAndInsertAll(name) {
            return get(name, { id: -1, title: "Все" });
        }

        // private helper
        function forceArray(source, delimiter) {
            var DEFAULT_DELIMITER = ",",
                isArr = angular.isArray(source);
            
            if (!isArr && !angular.String(source))
                throw "Type is not supported. Arg should be array or string";

            return isArr ? source : source.split(delimiter || DEFAULT_DELIMITER);
        }

        function populateLists(lists, obj) {
            obj = obj || {};
            var items = forceArray(lists);

            items.forEach(function (item) {
                obj[item] = get(item);
            });
            return obj;
        }

        function get(name, whatToInclude) {
            var arr = globalVariables[name];
            if (!arr)
                return undefined;

            if (whatToInclude) {
                if (angular.isArray(whatToInclude)) {
                    var result = whatToInclude.concat(arr);
                    return result;
                }
                else {
                    var result = angular.copy(arr);
                    result.splice(0, 0, whatToInclude);
                    return result;
                }
            }
            else
                return angular.copy(globalVariables[name]);
        }

        function getFilter(name) {
            var deferred = $q.defer();
            deferred.resolve(angular.copy($rootScope.globals[name]));
            return deferred;
        };

        function getExcept(name, whatToExclude, idProperty) {
            var items = get(name);
            if (whatToExclude && whatToExclude.length) {
                var path = idProperty || "id";
                var ids = whatToExclude.map(function(item) { return getProperty(item, path); });
                items = _.filter(items, (item) => ids.indexOf(item.id) < 0);
            }
            return items;
        }
        
        function getProperty(obj, path) {
            var arr = path.split(".");
            if (arr.length === 1)
                return obj[arr];
            else return traverse(obj, arr);

            function traverse(obj, arr) {
                arr.forEach(function (key) { obj = obj[key]; });
                return obj;
            }
        }

        function clear() {
            globalVariables = null;
        }
    }
})();