(function () {
	// TODO: possible issue: rate remembered in localstorate may override rate from here.
	// May be I need to disable remembering rate  if order is from template? 
		
	angular.module('orderTemplate')
	.service('orderTemplateFromOrderCreateModel', orderTemplateFromOrderCreateModel);
	
	function orderTemplateFromOrderCreateModel() {
		// takes current 
		var defaultConfig = {
			rememberCustomer: false,
			rememberContactPerson: false,
			rememberJobs: {
				languageId: false,
				rate: false
			}
		}
		return {
			create: create
		};
		
		function pushJobs(jobs, jobsConfig) {
			if(!jobsConfig) 
				return [];
			return jobs.map(function(item) {
				return {
					jobTypeId: item.jobTypeId,
					languageId: jobsConfig.languageId ? item.languageId : null,
					rate: jobsConfig.rate ? item.initial && item.initial.pricing && item.initial.pricing.rate : null
				}
			});
		}
		
		function create(orderCreateModel, config) {
			config = config || defaultConfig;
			
			if (!orderCreateModel) 
				throw new Error('no orderCreateModelSupplied');
			var model = orderCreateModel;
			var template = {
				customerId: config.rememberCustomer ? model.customerId : null,
				contactPerson: config.rememberContactPerson ? model.contactPerson : null,
				jobs: pushJobs(model.jobs, config.rememberJobs)
			};
			
			return template; 
		}
	}
})();