﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using System.Windows.Input;
using System.ComponentModel;
using MVVMFirma.Validators;
using System.Diagnostics;
using System.Xml.Linq;

namespace MVVMFirma.ViewModels
{
    public class NewOrderViewModel : OneViewModel<Orders>
    {
        #region Contructor
        public NewOrderViewModel()
            : base("Order")
        {
            item = new Orders();
            Messenger.Default.Register<CustomersForAllView>(this, getChosenCustomer);
        }
        #endregion

        #region Command
        private BaseCommand _ShowCustomers;
        public ICommand ShowCustomers
        {
            get
            {
                if (_ShowCustomers == null)
                    _ShowCustomers = new BaseCommand(() => showCustomers());
                return _ShowCustomers;
            }
        }
        
        private void showCustomers()
        {
            Messenger.Default.Send<string>("CustomersAll");
        }

        #endregion

        #region Fields
        public int? CustomerID
        {
            get
            { return item.CustomerID; }
            set
            {
                item.CustomerID = value;
                OnPropertyChanged(() => CustomerID);
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime? OrderDate
        {
            get
            { return item.OrderDate; }
            set
            {
                item.OrderDate = value;
                OnPropertyChanged(() => OrderDate);
            }
        }

        public DateTime? ShippedDate
        {
            get
            { return item.ShippedDate; }
            set
            {
                item.ShippedDate = value;
                OnPropertyChanged(() => ShippedDate);
            }
        }

        public int? PaymentMethodID
        {
            get
            { return item.PaymentMethodID; }
            set
            {
                item.PaymentMethodID = value;
                OnPropertyChanged(() => PaymentMethodID);
            }
        }

        #endregion

        #region Properties for ComboBox
        public IQueryable<KeyAndValue> PaymentMethodsItems
        {
            get
            {
                return new PaymentMethodB(pdabEntities).GetPaymentMethodKeyAndValueItems();
            }
        }
        #endregion

        #region Helpers
        private void getChosenCustomer(CustomersForAllView customer)
        {
            CustomerID = customer.CustomerID;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
        }
        
        public override void Save()
        {
            pdabEntities.Orders.Add(item);
            pdabEntities.SaveChanges();
        }
        #endregion
    }
}
