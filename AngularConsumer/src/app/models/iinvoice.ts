import { IProduct } from './iproduct';
import { IInvoiceItem } from './iinvoice-item';

export interface IInvoice {
    Id:number,
    Items:IInvoiceItem[],
    Total:number
}
