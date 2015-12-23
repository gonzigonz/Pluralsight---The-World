// tripsController.js
(function () {

	"use strict";

	angular.module("app-trips").controller("tripsController", tripController);

	function tripController() {

		var vm = this;

		vm.trips = [{
			name: "US Trip",
			created: new Date()
		}, {
			name: "World Trip",
			created: new Date()
		}];

		vm.newTrip = {}

		vm.addTrip = function () {

			alert(vm.newTrip.name);
		};

	}

})();