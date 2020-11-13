'use strict';

// Declare app level module which depends on views, and core components
angular.module('myApp', [
    'ngAnimate',
    'ngRoute',
    'ui.bootstrap',

    'myApp.home'
]).
    config(['$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {
        $locationProvider.hashPrefix('!');

        $routeProvider.otherwise({ redirectTo: '/' });
    }]);