(function () {
    angular.module('grades')
    .service('gradesService', gradesService);
    
    function gradesService($resource, $http, $q, _) {

        var url = '/api/grades/:id',
            Api = $resource(url, { 'id': '@id' });

        return {
            makeModel: makeModel,
            createModel: createModel,
            get: get,
            add: add,
            update: update,
            remove: remove,
            addDomain: addDomain
        }

        function addDomain(languageId, domainId, model) {
            var url = 'api/providerLanguage/' + languageId + '/Domains';
                
            return $http.post(url, model);
        }

        function add(model) {
            var item = new Api(model);
            return item.$save();          
        }

        function update(model) {
            var item = new Api(model);
            return item.$save();
        }

        function remove(id) {
            return Api.remove({ id: id });
        }

        function get($defer, params) {
            console.log('grades.get');
            
            Api.get(params.url(), updateView);
            
            function updateView(data) {
                console.log('received grades list');
                console.log(data);
                params.total(data.total);
                params.payload = data.payload; // payload = avg(score)
                $defer.resolve(data.result);
            }
        }

        // make model out of view model from gradelist.tpl (=GradeViewModel.cs)
        function makeModel(item) {
            return {
                id : item.id,
                providerId: item.provider.id,
                legacyJobName: item.jobPart.name,
                score: item.score,
                languageId: item.language ? item.language.id : null,
                domain1Id: item.domain1 ? item.domain1.id : null,
                domain2Id: item.domain2 ? item.domain2.id : null,
                // TODO: serviceTypeId should be real !!! 
                serviceTypeId: null,
                comment: item.comment,
                error: item.error || {},
                bonus: item.bonus || {}
            };
        }

        function createModel(config) {
            var model = {
                id: null,
                providerId: null,
                legacyJobName: null,
                languageId: null,
                domain1Id: null,
                domain2Id: null,
                score: 8, // default score is 8
                serviceTypeId: 1, // default = written translation
                comment: null,
                error: {},
                bonus: {}
            }

            return _.extend(model, config);
        }
    }

})();