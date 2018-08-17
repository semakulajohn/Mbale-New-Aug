function configState($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(false);
    
    // Set default state
    $urlRouterProvider

        .otherwise("/dashboard");  

    $stateProvider
          // Dashboard - Main page
        .state('dashboard', {
            url: "/dashboard",
            templateUrl: "/app/views/dashboard.html",
            data: {
                pageTitle: 'Dashboard',
               
            }
        })
   
         .state('login', {
             url: "/login",
             templateUrl: "/app/views/adminAccount/login/login.html",
             data: {
                 //pageTitle: 'Profile'
             }
         })
              // User Profile page
    .state('profile', {
        url: "/profile",
        templateUrl: "/app/views/_common/profile.html",
        data: {
            pageTitle: 'Profile'
        }
    })
     

    
    // Modules section 

         //suppliers
     .state('suppliers', {
         abstract: true,
         url: "/suppliers",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Suppliers'
         }
     })

    .state('suppliers.list', {
        url: "/suppliers",
        templateUrl: "/app/views/supplier/list.html",
        data: {
            pageTitle: 'Suppliers',
        },
        controller: function ($scope, $stateParams) {

        }
    })

             //branchManagers
     .state('branchmanagers', {
         abstract: true,
         url: "/branchmanagers",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Branch Managers'
         }
     })

    .state('branchmanagers.list', {
        url: "/branchmanagers",
        templateUrl: "/app/views/branchmanager/list.html",
        data: {
            pageTitle: 'Branch Managers',
        },
        controller: function ($scope, $stateParams) {

        }
    })
          .state('branch-user-edit', {
              url: "/branchmanagers/:userId",
              templateUrl: "/app/views/branchmanager/attachbranch.html",
              data: {
                  pageTitle: 'Attach Branch',
              },
              controller: function ($scope, $stateParams) {
                  $scope.action = $stateParams.action;
                  $scope.userId = $stateParams.userId;
                  $scope.defaultTab = 'edit'

              }
          })
        // creditors
     .state('creditors', {
         abstract: true,
         url: "/creditors",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Creditors'
         }
     })

    .state('creditors.list', {
        url: "/creditors",
        templateUrl: "/app/views/creditor/list.html",
        data: {
            pageTitle: 'Creditors',
        },
        controller: function ($scope, $stateParams) {

        }
    })
         .state('branch-creditors-list', {
             url: "/creditors/:branchId",
             templateUrl: "/app/views/creditor/branch-creditor-list.html",
             data: {
                 pageTitle: 'Branch Creditors',
             },
             controller: function ($scope, $stateParams) {
                 $scope.branchId = $stateParams.branchId;

             }
         })

          // debtors
     .state('debtors', {
         abstract: true,
         url: "/debtors",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Debtors'
         }
     })

    .state('debtors.list', {
        url: "/debtors",
        templateUrl: "/app/views/debtor/list.html",
        data: {
            pageTitle: 'Debtors',
        },
        controller: function ($scope, $stateParams) {

        }
    })

         .state('branch-debtors-list', {
             url: "/debtors/:branchId",
             templateUrl: "/app/views/debtor/branch-debtor-list.html",
             data: {
                 pageTitle: 'Branch Debtors',
             },
             controller: function ($scope, $stateParams) {
                 $scope.branchId = $stateParams.branchId;

             }
         })
           //customers
     .state('customers', {
         abstract: true,
         url: "/customers",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Customers'
         }
     })

    .state('customers.list', {
        url: "/customers",
        templateUrl: "/app/views/customer/list.html",
        data: {
            pageTitle: 'Customers',
        },
        controller: function ($scope, $stateParams) {

        }
    })

   //branches
     .state('branches', {
         abstract: true,
         url: "/branches",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Branches'
         }
     })

    .state('branches.list', {
        url: "/branches",
        templateUrl: "/app/views/branch/list.html",
        data: {
            pageTitle: 'Branches',
        },
        controller: function ($scope, $stateParams) {

        }
    })

          .state('branch-details-list', {
              url: "/branches/:branchId",
              templateUrl: "/app/views/branch/dashboard.html",
              data: {
                  pageTitle: 'Branch Details',
              },
              controller: function ($scope, $stateParams) {
                  $scope.branchId = $stateParams.branchId;

              }
          })


    .state('branch-edit', {
        url: "/branches/:action/:branchId",
        templateUrl: "/app/views/branch/edit.html",
        data: {
            pageTitle: 'Branch edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.branchId = $stateParams.branchId;
            $scope.defaultTab = 'edit';
        }
    })


   //inventories
     .state('inventories', {
         abstract: true,
         url: "/inventories",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Inventories'
         }
     })

    .state('inventories.list', {
        url: "/inventories",
        templateUrl: "/app/views/inventory/list.html",
        data: {
            pageTitle: 'Inventories',
        },
        controller: function ($scope, $stateParams) {

        }
    })

    .state('inventory-edit', {
        url: "/inventories/:action/:inventoryId",
        templateUrl: "/app/views/inventory/edit.html",
        data: {
            pageTitle: 'Inventory edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.inventoryId = $stateParams.inventoryId;
            $scope.defaultTab = 'edit';
        }
    })
         .state('inventory-store', {
             url: "/inventories/:storeId",
             templateUrl: "/app/views/inventory/store-inventory-list.html",
             data: {
                 pageTitle: 'Store Inventory',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.storeId = $stateParams.storeId;
                 $scope.defaultTab = 'edit';
             }
         })

         .state('inventory-store-edit', {
             url: "/inventories/:action/:inventoryId/:storeId",
             templateUrl: "/app/views/inventory/edit.html",
             data: {
                 pageTitle: 'Store Inventory',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.action = $stateParams.action;
                 $scope.inventoryId = $stateParams.inventoryId;
                 $scope.storeId = $stateParams.storeId;
                 $scope.defaultTab = 'edit';
             }
         })
         //stores
     .state('stores', {
         abstract: true,
         url: "/stores",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Stores'
         }
     })

    .state('stores.list', {
        url: "/stores",
        templateUrl: "/app/views/store/list.html",
        data: {
            pageTitle: 'Stores',
        },
        controller: function ($scope, $stateParams) {

        }
    })

        .state('branch-stores-list', {
            url: "/stores/branch/:branchId",
            templateUrl: "/app/views/store/branch-store-list.html",
            data: {
                pageTitle: 'Branch Store',
            },
            controller: function ($scope, $stateParams) {
                $scope.branchId = $stateParams.branchId;
            }
        })

    .state('store-edit', {
        url: "/stores/:action/:storeId",
        templateUrl: "/app/views/store/edit.html",
        data: {
            pageTitle: 'Store edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.storeId = $stateParams.storeId;
            $scope.defaultTab = 'edit';
        }
    })

          .state('store-flourStanding', {
              url: "/store/storeStanding/:storeId",
              templateUrl: "/app/views/store/storeStanding.html",
              data: {
                  pageTitle: 'Store Details',
              },
              controller: function ($scope, $stateParams) {
                  $scope.storeId = $stateParams.storeId;
                  $scope.defaultTab = 'Store Standing';

              }
          })
         
           //activities
     .state('activities', {
         abstract: true,
         url: "/activities",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Activities'
         }
     })

    .state('activities.list', {
        url: "/activities",
        templateUrl: "/app/views/activity/list.html",
        data: {
            pageTitle: 'Activities',
        },
        controller: function ($scope, $stateParams) {

        }
    })

    .state('activity-edit', {
        url: "/activities/:action/:activityId",
        templateUrl: "/app/views/activity/edit.html",
        data: {
            pageTitle: 'Activity edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.activityId = $stateParams.activityId;
            $scope.defaultTab = 'edit';
        }
    })
         //supplies
     .state('supplies', {
         abstract: true,
         url: "/supplies",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Supplies'
         }
     })

    .state('supplies.list', {
        url: "/supplies",
        templateUrl: "/app/views/supply/list.html",
        data: {
            pageTitle: 'Supplies',
        },
        controller: function ($scope, $stateParams) {

        }
    })

          .state('supplier-supply-list', {
              url: "/supplies/:supplierId",
              templateUrl: "/app/views/supply/supplier-supply.html",
              data: {
                  pageTitle: 'Supplier Supplies',
              },
              controller: function ($scope, $stateParams) {
                  $scope.supplierId = $stateParams.supplierId;

              }
          })

         .state('branch-supply-list', {
             url: "/supplies/branch/:branchId",
             templateUrl: "/app/views/supply/list.html",
             data: {
                 pageTitle: 'Branch Supplies',
             },
             controller: function ($scope, $stateParams) {
                 $scope.branchId = $stateParams.branchId;

             }
         })

         .state('supplier-supply-edit', {
             url: "/supplies/:action/:supplierId/:supplyId",
             templateUrl: "/app/views/supply/edit.html",
             data: {
                 pageTitle: 'Supplier Supplies Edit',
             },
             controller: function ($scope, $stateParams) {
                 $scope.action = $stateParams.action;
                 $scope.supplierId = $stateParams.supplierId;
                 $scope.supplyId = $stateParams.supplyId;
                 $scope.defaultTab = 'edit'

             }
         })
    .state('supply-edit', {
        url: "/supplies/:action/:supplyId",
        templateUrl: "/app/views/supply/edit.html",
        data: {
            pageTitle: 'Supply edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.supplyId = $stateParams.supplyId;
            $scope.defaultTab = 'edit';
        }
    })


               //transactions
     .state('transactions', {
         abstract: true,
         url: "/transactions",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Transactions'
         }
     })

    .state('transactions.list', {
        url: "/transactions",
        templateUrl: "/app/views/transaction/list.html",
        data: {
            pageTitle: 'Transactions',
        },
        controller: function ($scope, $stateParams) {

        }
    })

          .state('branch-transaction-list', {
              url: "/transactions/branch/:branchId",
              templateUrl: "/app/views/transaction/branch-transaction-list.html",
              data: {
                  pageTitle: 'Branch Transactions',
              },
              controller: function ($scope, $stateParams) {
                  $scope.branchId = $stateParams.branchId;
              }
          })

    .state('transaction-edit', {
        url: "/transactions/:action/:transactionId",
        templateUrl: "/app/views/transaction/edit.html",
        data: {
            pageTitle: 'Transaction edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.transactionId = $stateParams.transactionId;
            $scope.defaultTab = 'edit';
        }
    })

          .state('transanctionType-transactions', {
              url: "/transactions/:transactionTypeId",
              templateUrl: "/app/views/transaction/transactionType-transactions.html",
              data: {
                  pageTitle: 'Transactions',
                  pageDesc: ''
              },
              controller: function ($scope, $stateParams) {
                  $scope.transactionTypeId = $stateParams.transactionTypeId;
              }
          })
   
        //batches
     .state('batches', {
         abstract: true,
         url: "/batches",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Batches'
         }
     })

    .state('batches.list', {
        url: "/batches",
        templateUrl: "/app/views/batch/list.html",
        data: {
            pageTitle: 'Batches',
        },
        controller: function ($scope, $stateParams) {

        }
    })


    .state('branch-batches-list', {
        url: "/batches/branch/:branchId",
        templateUrl: "/app/views/batch/branch-batch-list.html",
        data: {
            pageTitle: 'Branch Batches',
        },
        controller: function ($scope, $stateParams) {
            $scope.branchId = $stateParams.branchId;
        }
    })

    .state('batch-edit', {
        url: "/batches/:action/:batchId",
        templateUrl: "/app/views/batch/edit.html",
        data: {
            pageTitle: 'Batch edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.batchId = $stateParams.batchId;
            $scope.defaultTab = 'edit';
        }
    })


         .state('batch-detail', {
             url: "/batch/detail/:batchId",
             templateUrl: "/app/views/batch/detail.html",
             data: {
                 pageTitle: 'Batch Details',
             },
             controller: function ($scope, $stateParams) {
                 $scope.batchId = $stateParams.batchId;
                 $scope.defaultTab = 'detail';

             }
         })

         .state('batchoutput-batch', {
             url: "/batchoutputs/:batchId",
             templateUrl: "/app/views/batchoutput/batch-batchoutput-list.html",
             data: {
                 pageTitle: 'Batch OutPut',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.batchId = $stateParams.batchId;
                 $scope.defaultTab = 'edit';
             }
         })

         .state('batchoutput-batch-edit', {
             url: "/batchoutputs/:action/:batchOutPutId/:batchId",
             templateUrl: "/app/views/batchoutput/edit.html",
             data: {
                 pageTitle: 'Batch OutPut',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.action = $stateParams.action;
                 $scope.batchOutPutId = $stateParams.batchOutPutId;
                 $scope.batchId = $stateParams.batchId;
                 $scope.defaultTab = 'edit';
             }
         })


         .state('factoryExpense-batch', {
             url: "/factoryExpenses/:batchId",
             templateUrl: "/app/views/factoryExpense/batch-factoryExpense-list.html",
             data: {
                 pageTitle: 'Batch Factory Expenses',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.batchId = $stateParams.batchId;
                 $scope.defaultTab = 'edit';
             }
         })

         .state('factoryExpense-batch-edit', {
             url: "/factoryExpenses/:action/:factoryExpenseId/:batchId",
             templateUrl: "/app/views/factoryExpense/edit.html",
             data: {
                 pageTitle: 'Batch Factory Expense',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.action = $stateParams.action;
                 $scope.factoryExpenseId = $stateParams.factoryExpenseId;
                 $scope.batchId = $stateParams.batchId;
                 $scope.defaultTab = 'edit';
             }
         })


         .state('otherExpense-batch', {
             url: "/otherExpenses/:batchId",
             templateUrl: "/app/views/otherExpense/batch-otherExpense-list.html",
             data: {
                 pageTitle: 'Batch Other Expenses',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.batchId = $stateParams.batchId;
                 $scope.defaultTab = 'edit';
             }
         })

         .state('otherExpense-batch-edit', {
             url: "/otherExpenses/:action/:otherExpenseId/:batchId",
             templateUrl: "/app/views/otherExpense/edit.html",
             data: {
                 pageTitle: 'Batch Other Expense',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.action = $stateParams.action;
                 $scope.otherExpenseId = $stateParams.otherExpenseId;
                 $scope.batchId = $stateParams.batchId;
                 $scope.defaultTab = 'edit';
             }
         })

          .state('machineRepair-batch', {
              url: "/machineRepairs/:batchId",
              templateUrl: "/app/views/machinerepair/batch-machineRepair-list.html",
              data: {
                  pageTitle: 'Batch Factory Expenses',
                  pageDesc: ''
              },
              controller: function ($scope, $stateParams) {
                  $scope.batchId = $stateParams.batchId;
                  $scope.defaultTab = 'edit';
              }
          })

         .state('machineRepair-batch-edit', {
             url: "/machineRepairs/:action/:machinerepairId/:batchId",
             templateUrl: "/app/views/machinerepair/edit.html",
             data: {
                 pageTitle: 'Batch Machine Repair',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.action = $stateParams.action;
                 $scope.machinerepairId = $stateParams.machinerepairId;
                 $scope.batchId = $stateParams.batchId;
                 $scope.defaultTab = 'edit';
             }
         })

         .state('labourCost-batch', {
             url: "/labourCosts/:batchId",
             templateUrl: "/app/views/labourcost/batch-labourCost-list.html",
             data: {
                 pageTitle: 'Batch Labour Cost',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.batchId = $stateParams.batchId;
                 $scope.defaultTab = 'edit';
             }
         })

         .state('labourCost-batch-edit', {
             url: "/labourCosts/:action/:labourCostId/:batchId",
             templateUrl: "/app/views/labourcost/edit.html",
             data: {
                 pageTitle: 'Batch Labour Cost',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.action = $stateParams.action;
                 $scope.labourCostId = $stateParams.labourCostId;
                 $scope.batchId = $stateParams.batchId;
                 $scope.defaultTab = 'edit';
             }
         })

   //sectors
     .state('sectors', {
         abstract: true,
         url: "/sectors",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Departments'
         }
     })

    .state('sectors.list', {
        url: "/sectors",
        templateUrl: "/app/views/sector/list.html",
        data: {
            pageTitle: 'Departments',
        },
        controller: function ($scope, $stateParams) {

        }
    })

    .state('sector-edit', {
        url: "/sectors/:action/:sectorId",
        templateUrl: "/app/views/sector/edit.html",
        data: {
            pageTitle: 'Department edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.sectorId = $stateParams.sectorId;
            $scope.defaultTab = 'edit';
        }
    })
          //transactionSubtypes
     .state('transactionSubTypes', {
         abstract: true,
         url: "/transactionSubTypes",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'TransactionSubTypes'
         }
     })

    .state('transactionSubTypes.list', {
        url: "/transactionSubTypes",
        templateUrl: "/app/views/transactionSubType/list.html",
        data: {
            pageTitle: 'TransactionSubTypes',
        },
        controller: function ($scope, $stateParams) {

        }
    })

    .state('transactionSubType-edit', {
        url: "/transactionSubTypes/:action/:transactionSubTypeId",
        templateUrl: "/app/views/transactionSubType/edit.html",
        data: {
            pageTitle: 'TransactionSubType edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.transactionSubTypeId = $stateParams.transactionSubTypeId;
            $scope.defaultTab = 'edit';
        }
    })

        //CasualActivity
        .state('casualActivities', {
            abstract: true,
            url: "/casualActivities",
            templateUrl: "/app/views/_common/content_empty.html",
            data: {
                pageTitle: 'CasualActivities'
            }
        })

    .state('casualActivities-list', {
        url: "/casualActivities/:accountId",
        templateUrl: "/app/views/casualactivity/casualworker-casualactivity-list.html",
        data: {
            pageTitle: 'Casual Activities',
        },
        controller: function ($scope, $stateParams) {
            $scope.accountId = $stateParams.accountId;
        }
    })

    .state('casualactivity-edit', {
        url: "/casualactivities/:action/:accountId/:casualActivityId",
        templateUrl: "/app/views/casualactivity/edit.html",
        data: {
            pageTitle: 'Casual Activity edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.accountId = $stateParams.accountId;
            $scope.casualActivityId = $stateParams.casualActivityId;
            $scope.defaultTab = 'edit';
        }
    })


    //casualworkers
     .state('casualWorkers', {
         abstract: true,
         url: "/casualWorkers",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'CasualWorkers'
         }
     })

    .state('casualWorkers.list', {
        url: "/casualWorkers",
        templateUrl: "/app/views/casualWorker/list.html",
        data: {
            pageTitle: 'CasualWorkers',
        },
        controller: function ($scope, $stateParams) {

        }
    })

            .state('branch-casualWorker-list', {
                url: "/casualWorkers/:branchId",
                templateUrl: "/app/views/casualWorker/branchCasualWorkers.html",
                data: {
                    pageTitle: 'Branch Casual Workers',
                },
                controller: function ($scope, $stateParams) {
                    $scope.branchId = $stateParams.branchId;

                }
            })
    .state('casualWorker-edit', {
        url: "/casualWorkers/:action/:casualWorkerId",
        templateUrl: "/app/views/casualWorker/edit.html",
        data: {
            pageTitle: 'CasualWorker edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.casualWorkerId = $stateParams.casualWorkerId;
            $scope.defaultTab = 'edit';
        }
    })

            .state('casualworker-detail', {
                url: "/casualworker/detail/:casualWorkerId",
                templateUrl: "/app/views/casualworker/detail.html",
                data: {
                    pageTitle: 'Casual Worker Details',
                },
                controller: function ($scope, $stateParams) {
                    $scope.casualWorkerId = $stateParams.casualWorkerId;
                    $scope.defaultTab = 'detail';

                }
            })

        //requistions
     .state('requistions', {
         abstract: true,
         url: "/requistions",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Requistions'
         }
     })

    .state('requistions.list', {
        url: "/requistions",
        templateUrl: "/app/views/requistion/list.html",
        data: {
            pageTitle: 'Requistions',
        },
        controller: function ($scope, $stateParams) {

        }
    })

          .state('branch-requistion-list', {
              url: "/requistions/branch/:branchId",
              templateUrl: "/app/views/requistion/branch-requistion-list.html",
              data: {
                  pageTitle: 'Branch Requistions',
              },
              controller: function ($scope, $stateParams) {
                  $scope.branchId = $stateParams.branchId;
              }
          })

    .state('requistion-edit', {
        url: "/requistions/:action/:requistionId",
        templateUrl: "/app/views/requistion/edit.html",
        data: {
            pageTitle: 'Requistion edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.requistionId = $stateParams.requistionId;
            $scope.defaultTab = 'edit';
        }
    })

    .state('status-requistions', {
         url: "/requistions/:statusId",
            templateUrl: "/app/views/requistion/status-requistions.html",
        data: {
            pageTitle: 'Requistions',
            pageDesc: ''
         },
         controller: function ($scope, $stateParams) {
            $scope.statusId = $stateParams.statusId;
        }
    })

    //products
     .state('products', {
         abstract: true,
         url: "/products",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'products'
         }
     })

    .state('products.list', {
        url: "/products",
        templateUrl: "/app/views/product/list.html",
        data: {
            pageTitle: 'products',
        },
        controller: function ($scope, $stateParams) {

        }
    })

    .state('product-edit', {
        url: "/products/:action/:productId",
        templateUrl: "/app/views/product/edit.html",
        data: {
            pageTitle: 'product edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.productId = $stateParams.productId;
            $scope.defaultTab = 'edit';
        }
    })
         //stocks
     .state('stocks', {
         abstract: true,
         url: "/stocks",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'stocks'
         }
     })

    .state('stocks.list', {
        url: "/stocks",
        templateUrl: "/app/views/stock/list.html",
        data: {
            pageTitle: 'stocks',
        },
        controller: function ($scope, $stateParams) {

        }
    })
      

           .state('stock-detail', {
             url: "/stock/detail/:stockId",
             templateUrl: "/app/views/stock/detail.html",
             data: {
                 pageTitle: 'Stock Details',
             },
             controller: function ($scope, $stateParams) {
                 $scope.stockId = $stateParams.stockId;
                 $scope.defaultTab = 'detail';

             }
           })

         .state('stock-store', {
             url: "/stocks/:storeId",
             templateUrl: "/app/views/stock/store-stock-list.html",
             data: {
                 pageTitle: 'Store Stock',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.storeId = $stateParams.storeId;
                 $scope.defaultTab = 'edit';
             }
         })

    //orders
     .state('orders', {
         abstract: true,
         url: "/orders",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'orders'
         }
     })

    .state('orders.list', {
        url: "/orders",
        templateUrl: "/app/views/order/list.html",
        data: {
            pageTitle: 'orders',
        },
        controller: function ($scope, $stateParams) {

        }
    })

          .state('branch-order-list', {
              url: "/orders/branch/:branchId",
              templateUrl: "/app/views/order/branch-order-list.html",
              data: {
                  pageTitle: 'Branch Orders',
              },
              controller: function ($scope, $stateParams) {
                  $scope.branchId = $stateParams.branchId;
              }
          })

         .state('customerorders-list', {
             url: "/orders/:customerId",
             templateUrl: "/app/views/order/customer-orders.html",
             data: {
                 pageTitle: 'orders',
             },
             controller: function ($scope, $stateParams) {
                 $scope.customerId = $stateParams.customerId;
             }
         })

    .state('order-edit', {
        url: "/orders/:action/:customerId/:orderId",
        templateUrl: "/app/views/order/edit.html",
        data: {
            pageTitle: 'order edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.customerId = $stateParams.customerId;
            $scope.orderId = $stateParams.orderId;
            $scope.defaultTab = 'edit';
        }
    })

         .state('order-detail', {
             url: "/order/detail/:orderId",
             templateUrl: "/app/views/order/detail.html",
             data: {
                 pageTitle: 'Order Details',
             },
             controller: function ($scope, $stateParams) {
                 $scope.orderId = $stateParams.orderId;
                 $scope.defaultTab = 'detail';

             }
         })
     //deliveries
     .state('deliveries', {
         abstract: true,
         url: "/deliveries",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'deliveries'
         }
     })

    .state('deliveries.list', {
        url: "/deliveries",
        templateUrl: "/app/views/delivery/list.html",
        data: {
            pageTitle: 'deliveries',
        },
        controller: function ($scope, $stateParams) {

        }
    })

    .state('delivery-edit', {
        url: "/deliveries/:action/:customerId/:orderId/:deliveryId",
        templateUrl: "/app/views/delivery/edit.html",
        data: {
            pageTitle: 'delivery edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.deliveryId = $stateParams.deliveryId;
            $scope.customerId = $stateParams.customerId;
            $scope.orderId = $stateParams.orderId;
            $scope.defaultTab = 'edit';
        }
    })
         .state('delivery-order-list', {
             url: "/deliveries/:orderId",
             templateUrl: "/app/views/delivery/order-delivery-list.html",
             data: {
                 pageTitle: 'Order Delivery list',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
               
                 $scope.orderId = $stateParams.orderId;
                
             }
         })

      .state('delivery-branch-list', {
          url: "/deliveries/branch/:branchId",
          templateUrl: "/app/views/delivery/branch-delivery-list.html",
          data: {
              pageTitle: 'Branch Delivery list',
              pageDesc: ''
          },
          controller: function ($scope, $stateParams) {

              $scope.branchId = $stateParams.branchId;

          }
      })
    

    //machinerepairs
     .state('machinerepairs', {
         abstract: true,
         url: "/machinerepairs",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'machinerepairs'
         }
     })

    .state('machinerepairs.list', {
        url: "/machinerepairs",
        templateUrl: "/app/views/machinerepair/list.html",
        data: {
            pageTitle: 'machinerepairs',
        },
        controller: function ($scope, $stateParams) {

        }
    })

    .state('machinerepair-edit', {
        url: "/machinerepairs/:action/:machinerepairId",
        templateUrl: "/app/views/machinerepair/edit.html",
        data: {
            pageTitle: 'machinerepair edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.machinerepairId = $stateParams.machinerepairId;
            $scope.defaultTab = 'edit';
        }
    })

               //reports
     .state('reports', {
         abstract: true,
         url: "/reports",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Reports'
         }
     })
        
     .state('reports.transactionlist', {
         url: "/reports",
         templateUrl: "/app/views/report/transactions.html",
         data: {
             pageTitle: 'Transactions',
         },
         controller: function ($scope, $stateParams) {

         }
     })


     .state('reports.supplylist', {
         url: "/reports",
         templateUrl: "/app/views/report/supplies.html",
         data: {
             pageTitle: 'Supplies',
         },
         controller: function ($scope, $stateParams) {

         }
     })

     //Account Transaction Activities
     .state('accounttransactionactivities', {
         abstract: true,
         url: "/accounttransactionactivities",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Account Transaction Activities'
         }
     })

    .state('accounttransactionactivities.list', {
        url: "/accounttransactionactivities",
        templateUrl: "/app/views/transactionactivity/list.html",
        data: {
            pageTitle: 'Account Transaction Activities',
        },
        controller: function ($scope, $stateParams) {

        }
    })
          .state('account-accounttransactionactivities-list', {
              url: "/accounttransactionactivities/:accountId",
              templateUrl: "/app/views/transactionactivity/account-accounttransactionactivities.html",
              data: {
                  pageTitle: 'Account Transaction Activities',
              },
              controller: function ($scope, $stateParams) {
                  $scope.accountId = $stateParams.accountId;

              }
          })

         .state('account-accounttransactionactivities-edit', {
             url: "/accounttransactionactivities/:action/:accountId/:transactionActivityId",
             templateUrl: "/app/views/transactionactivity/edit.html",
             data: {
                 pageTitle: 'Account Transaction Activity Edit',
             },
             controller: function ($scope, $stateParams) {
                 $scope.action = $stateParams.action;
                 $scope.accountId = $stateParams.accountId;
                 $scope.transactionActivityId = $stateParams.transactionActivityId;
                 $scope.defaultTab = 'edit'

             }
         })
    .state('accounttransactionactivities-edit', {
        url: "/accounttransactionactivities/:action/:transactionActivityId",
        templateUrl: "/app/views/transactionactivity/edit.html",
        data: {
            pageTitle: 'Account Transaction Activity Edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.transactionActivityId = $stateParams.transactionActivityId;
            $scope.defaultTab = 'edit';
        }
    })
    //Search
    $stateProvider
    .state('search', {
        url: "/search/:q",
        templateUrl: "/app/views/search/index.html",
        data: {
            pageTitle: 'Search'
        },
        controller: function ($scope, $stateParams) {
            $scope.q = $stateParams.q;
        }
    })

}

angular
    .module('homer')
    .config(configState).run(function ($rootScope, $state) {
        $rootScope.$state = $state;
      
        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            if (next.match("/UsersAdmin/")) {
                var parts = next.split('#');
                if (parts.length > 1) {
                    if (!next.match('#/dashboard')) {
                        window.location = '/#' + parts[1];
                    }
                }
            }
        });

    })
  