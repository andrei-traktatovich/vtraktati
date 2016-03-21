(function () {
    angular.module('order.list')
    .service('jobsViewModelToGridTransformer', jobsViewModelToGridTransformer);

    function jobsViewModelToGridTransformer(constants) {
        return {
            transform: transform
        };

        /*  Requirements:
            - transform array of job graphs to a treeview grid model
            - assign $$treeLevel according to the depth
            - assign type according to the type of the entity (child entity)
            - job: assign "parent/child" depending on its kind 
            
            Each row: 
            - extend each row with $$treeLevel and type field
            
        */
        function transform(jobs, includeDaughterJobs) {
            if (includeDaughterJobs === undefined)
                includeDaughterJobs = false;

            var rows = [],
                INITIAL_TREE_LEVEL = 0,
                JOB_GRID_ROW_TYPE_JOB = constants.ROW_TYPES.JOB_GRID_ROW_TYPE_JOB,
                JOB_GRID_ROW_TYPE_PARTICIPANT = constants.ROW_TYPES.JOB_GRID_ROW_TYPE_PARTICIPANT;

            jobs.forEach(function (item) {
                pushJob(item, INITIAL_TREE_LEVEL);
            });

            return rows;

            function pushJob(item, treeLevel) {
                item.$$treeLevel = treeLevel;
                item.type = JOB_GRID_ROW_TYPE_JOB;
                rows.push(item);

                if (item.jobParts && item.jobParts.length)
                    pushJobParts(item.jobParts, treeLevel + 1);
            }

            function pushJobParts(jobParts, treeLevel) {
                jobParts.forEach(function (item) {
                    pushJobPart(item, treeLevel);
                });
            }

            function pushJobPart(item, treeLevel) {
                item.$$treeLevel = treeLevel;
                item.type = JOB_GRID_ROW_TYPE_PARTICIPANT;
                rows.push(item);
                if (includeDaughterJobs && item.daughterJob)
                    pushJob(item.daughterJob, treeLevel + 1);
            }

            return rows;
        }
    }

})();