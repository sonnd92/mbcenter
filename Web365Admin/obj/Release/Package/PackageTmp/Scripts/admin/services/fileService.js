﻿//create by BienLV2
//15-07-2014
//services use to product type service
'use strict';
web365app.factory('fileService', ['$q', '$http', function ($q, $http) {

    var fileService = {};

    //function get list product type
    var _getList = function (obj) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/File/GetList', {
            params: obj
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _getPropertyFilter = function () {

        var deferred = $q.defer();

        //get data from services
        $http.get('/File/GetPropertyFilter').success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _getListByArrayID = function (obj) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/File/GetListByArrayID', {
            params: {
                arrID: obj
            }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get list for tree product type
    var _getListForTree = function (obj) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/File/GetListForTree').success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get detail form picture type
    var _getDetailForm = function (id) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/File/Detail', {
            params: {
                id: id
            }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get edit form product type
    var _getEditForm = function (id) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/File/EditForm', {
            params: {
                id: id
            }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get edit form product type
    var _modified = function (obj) {

        var deferred = $q.defer();

        //get data from services
        $http.post('/File/Action', obj, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _showAll = function (listId) {

        var deferred = $q.defer();

        $http.post('/File/ShowAll', {
            listId: listId
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _hideAll = function (listId) {

        var deferred = $q.defer();

        $http.post('/File/HideAll', {
            listId: listId
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _deleteAll = function (listId) {

        var deferred = $q.defer();

        $http.post('/File/Delete', {
            listId: listId
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    fileService.getList = _getList;
    fileService.getPropertyFilter = _getPropertyFilter;
    fileService.getListByArrayID = _getListByArrayID;
    fileService.getListForTree = _getListForTree;
    fileService.getDetailForm = _getDetailForm;
    fileService.getEditForm = _getEditForm;
    fileService.modified = _modified;
    fileService.showAll = _showAll;
    fileService.hideAll = _hideAll;
    fileService.deleteAll = _deleteAll;

    return fileService;
}]);