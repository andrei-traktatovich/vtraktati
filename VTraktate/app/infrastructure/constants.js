(function () {
    angular.module('constants', [])
	.service('constants', function () {

	    var jobStatusClassLocator = [
		  		'job-unknown',
		  		'job-created',
		  		'job-pending',
                'job-canceled',
                'job-delivered',
                'job-completed'
	    ];

	    var participantStatusClassLocator = [
		  		'job-unknown',
		  		'participant-created',
		  		'participant-pending',
		  		'participant-completed',
                'participant-canceled'
	    ];

	    return {
	        CURRENCIES: {
	            RUBLE: 1
	        },
            DEFAULTJOBUOM: 1,
	        DEFAULTSERVICEUOM: 1,
	        ROW_TYPES: {
	            JOB_GRID_ROW_TYPE_JOB: 0,
	            JOB_GRID_ROW_TYPE_PARTICIPANT: 1,
	        },
	        JOB_STATUSES: {
	            JOB_STATUS_CREATED: 1,
	            JOB_STATUS_PENDING: 2,
	            JOB_STATUS_CANCELED: 3,
	            JOB_STATUS_COMPLETED: 4,
	            JOB_STATUS_DELIVERED: 5
	        },
	        JOB_PART_STATUSES : {
	            JOB_PART_STATUS_CREATED: 1,
	            JOB_PART_STATUS_PENDING: 2,
	            JOB_PART_STATUS_COMPLETED: 3,
	            JOB_PART_STATUS_CANCELED: 4
	        },
	        JOB_STATUS_CLASS_LOCATOR: jobStatusClassLocator,
	        JOB_GRID_CLASS_LOCATOR: [
  				{ prefix: 'job-row ', variable: jobStatusClassLocator },
  				{ prefix: 'participant-row', variable: participantStatusClassLocator }
	        ]
	    };
	});
})();