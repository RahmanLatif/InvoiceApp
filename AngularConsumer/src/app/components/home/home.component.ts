import { Component, OnInit, ViewChild } from '@angular/core';
import { IProduct } from 'src/app/models/iproduct';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { InvoiceService } from 'src/app/services/invoice.service';
import { IInvoiceItem } from 'src/app/models/iinvoice-item';
import { NotifierService } from 'angular-notifier';
import { IInvoice } from 'src/app/models/iinvoice';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public displayedColumns = ['Name', 'Description', 'Quantity', 'UnitPrice', 'Total', 'Delete'];
  public dataSource = new MatTableDataSource<IInvoiceItem>();
  public products: IProduct[];
  invoiceNumberInput: number;
  invoiceNumber: number;
  invoiceTotal: number;
  @ViewChild(MatTable) table: MatTable<IInvoiceItem>;
  constructor(private invoiceService: InvoiceService, private notifier: NotifierService) {
    this.invoiceNumber = 0;
    this.invoiceTotal = 0;
  }

  ngOnInit(): void {
    this.products = [];
    this.invoiceService.getProducts().subscribe(res => {
      res.forEach(element => {
        var prd: IProduct = {
          Id: element.id,
          Name: element.name,
          Description: element.description,
          Quantity: element.availableQuantity,
          UnitPrice: element.unitPrice
        };
        this.products.push(prd)
      });
    },
      error => {
        this.showNotification("error", error.message);
      })
  }
  displayFn(prd: IProduct): string {
    return prd && prd.Name ? prd.Name : "";
  }
  showNotification(type: string, message: string): void {
    this.notifier.notify(type, message);
  }
  ItemChanged(id: number, ind: number) {
    var prd = this.products.find(p => p.Id == id);
    this.dataSource.data[ind].Id = prd.Id;
    this.dataSource.data[ind].Name = prd.Name;
    this.dataSource.data[ind].Description = prd.Description;
    this.dataSource.data[ind].UnitPrice = prd.UnitPrice;
    this.dataSource.data[ind].Quantity = 0;
    this.dataSource.data[ind].Total = 0;
    this.table.renderRows();
    this.UpdateInvoiceTotal();
  }
  QuantityChanged(newValue: any, ind: number) {
    console.log(newValue);
    if(newValue==""){
      newValue="0";
    }
    var totQuantity: number = 0;
    this.dataSource.data[ind].Quantity = Number.parseInt(newValue);
    this.dataSource.data.forEach(element => {
      if (element.Id == this.dataSource.data[ind].Id) {
        totQuantity += element.Quantity;
      }
    });
    var product: IProduct = this.products.find(prd => prd.Id == this.dataSource.data[ind].Id);
    if (totQuantity > product.Quantity) {
      var diffQuantity: number = product.Quantity - (totQuantity - Number.parseInt(newValue));
      this.dataSource.data[ind].Quantity = diffQuantity;
      this.notifier.notify("error", "There is not enough quantity !!");
    }
    this.dataSource.data[ind].Total =
      this.dataSource.data[ind].Quantity *
      this.dataSource.data[ind].UnitPrice;
    this.table.renderRows();
    this.UpdateInvoiceTotal();
  }

  UpdateInvoiceTotal() {
    this.invoiceTotal = 0;
    this.dataSource.data.forEach(element => {
      this.invoiceTotal += element.Total;
    });
  }
  ReadInvoice() {
    if (this.invoiceNumberInput == null || this.invoiceNumberInput == 0) {
      this.showNotification("error", "Invalid Invoice Number !!");
      return;
    }
    this.invoiceService.getInvoice(this.invoiceNumberInput).subscribe(res => {
      var prdList: IInvoiceItem[] = [];
      res.items.forEach(element => {
        var invItem: IInvoiceItem = {
          Id: element.id,
          Name: element.name,
          Description: element.description,
          Quantity: element.quantity,
          UnitPrice: element.unitPrice,
          Total: element.total
        };
        prdList.push(invItem);
      });
      this.dataSource = new MatTableDataSource<IInvoiceItem>(prdList);
      this.table.renderRows();
      this.invoiceNumber = this.invoiceNumberInput;
      this.UpdateInvoiceTotal();
    },
      error => {
        this.showNotification("error", error.message);
        this.invoiceNumberInput = this.invoiceNumber;
      });
  }
  SaveInvoice() {
    if (this.dataSource.data.length == 0) {
      this.showNotification("error", "Empty Invoice !!");
      return;
    }
    var ok: boolean = true;
    this.dataSource.data.forEach(element => {
      if (element.Id == 0) {
        ok = false;
      }
    });
    if (!ok) {
      this.showNotification("error", "Invalid Items !!");
      return;
    }
    if (this.invoiceNumber == 0) {
      var invoice: IInvoice = {
        Items: this.dataSource.data,
        Id: 0,
        Total: this.invoiceTotal,
      };
      this.invoiceService.AddInvoice(invoice).subscribe(res => {
        this.Reset();
      },
        error => {
          this.showNotification("error", error.message);
        });
    }
    else {
      var invoice: IInvoice = {
        Items: this.dataSource.data,
        Id: this.invoiceNumber,
        Total: this.invoiceTotal,
      };
      this.invoiceService.EditInvoice(this.invoiceNumber, invoice).subscribe(res => {
        this.Reset();
      },
        error => {
          this.showNotification("error", error.message);
        });
    }
  }
  DeleteInvoice() {
    if (this.invoiceNumber == 0) {
      this.showNotification("error", "Invalid Invoice Number");
      return;
    }
    this.invoiceService.DeleteInvoice(this.invoiceNumber).subscribe(res => {
      this.Reset();
    },
      error => {
        this.showNotification("error", error.message);
      });
  }
  Reset() {
    this.invoiceNumber = 0;
    this.invoiceNumberInput = 0;
    this.dataSource = new MatTableDataSource<IInvoiceItem>();
    this.table.renderRows();
    this.UpdateInvoiceTotal();
  }
  AddItem() {
    var prd: IInvoiceItem = {
      Id: 0,
      Name: '',
      Description: '',
      UnitPrice: 0,
      Quantity: 0,
      Total: 0
    };
    this.dataSource.data.push(prd);
    this.table.renderRows();
    this.UpdateInvoiceTotal();
  }

  DeleteItem(ind: number) {
    this.dataSource.data.splice(ind, 1);
    this.table.renderRows();
    this.UpdateInvoiceTotal();
  }
}
