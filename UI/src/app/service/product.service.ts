import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import {  Product, ProductSend } from '../model/porduct.model';
import { apiServer } from 'src/assets/config/config';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  url = apiServer.URL + "api/products"
  productChanges = new Subject<Product[]>()
  constructor(private http: HttpClient , private toastr:ToastrService, private router:Router) { }

  getProductList(): Observable<any[]> {
    return this.http.get<any[]>(this.url );
  }

  addProduct(product: FormData) {
    //console.log(product)
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    this.http.post<any>(this.url , product, {headers}).subscribe(()=>{
      this.emitData()
      this.router.navigate(['/product'])
      this.toastr.success("product added successfully")    
    })
  }

  updateProduct(id:string , product: FormData) {
    
    product.append('productId' , id);
    const headers =  new HttpHeaders();
    headers.append('Content-Type' , 'multipart/form-data')
    this.http.put<any>(this.url + `/${id}` , product , {headers}).subscribe(()=>{
      this.emitData()
      this.router.navigate(['/product'])
      this.toastr.success("product updated successfully")
    })
  }

  deleteProductById(id: string) {
    this.http.delete<any>(`${this.url}/${id}`).subscribe(()=>{
      this.emitData()
      this.toastr.success("product deleted successfully")
    })
  }

  getProductDetailsById(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.url}/GetProduct/${id}`);
  }

  emitData(){
    this.getProductList().subscribe(data=>{
      this.productChanges.next(data)
    })
  }
  
}
