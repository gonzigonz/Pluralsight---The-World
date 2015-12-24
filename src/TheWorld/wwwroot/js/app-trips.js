// app-trips.js
(function () {
	
	"use strict";

	angular.module("app-trips", ["simpleControls", "ng-route"])
		.config(function ($routeProvider) {

			$routeProvider.when("/", {
				controller: "tripsController",
				controllerAs: "vm",
				templateUrl: "/view/tripsView.html"
			});

			$routeProvider.otherwise({
				redirectTo: "/"
			});
		});

})();