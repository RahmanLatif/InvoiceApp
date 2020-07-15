import { IProduct } from './iproduct';

export interface IInvoiceItem {
    Id:number,
    Name:string,
    Description:string,
    Quantity:number,
    UnitPrice:number,
    Total:number
}
