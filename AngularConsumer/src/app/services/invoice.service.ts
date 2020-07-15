import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from '../models/iproduct';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IInvoice } from '../models/iinvoice';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  constructor(private _http:HttpClient) { }
  getProducts():any{
    const httpOptions =  new HttpHeaders({
      'Content-Type': 'application/json',
     'Accept': ' */*',
    });
   return this._http.get(`${environment.ApiUrl}api/Home/GetProducts`,{headers:httpOptions});
  }
  getInvoice(Id:number):any{
    const httpOptions =  new HttpHeaders({
      'Content-Type': 'application/json',
       'Accept': ' */*',
      });
    return this._http.get(`${environment.ApiUrl}api/Home/${Id}`,{headers:httpOptions});
   }
   AddInvoice(invoice:IInvoice):any{
    const httpOptions =  new HttpHeaders({
      'Content-Type': 'application/json',
       'Accept': ' */*',
      });
     return this._http.post(`${environment.ApiUrl}api/Home`,invoice,{headers:httpOptions});
    }
    EditInvoice(id:number,invoice:IInvoice):any{
      const httpOptions =  new HttpHeaders({
        'Content-Type': 'application/json',
         'Accept': ' */*',
        });
       return this._http.put(`${environment.ApiUrl}api/Home/${id}`,invoice,{headers:httpOptions});
      }
      DeleteInvoice(id:number):any{
        const httpOptions =  new HttpHeaders({
          'Content-Type': 'application/json',
           'Accept': ' */*',
          });
         return this._http.delete(`${environment.ApiUrl}api/Home/${id}`,{headers:httpOptions});
        }
}
