import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ProductService } from '../service/product.service';
import { Product } from '../model/porduct.model';

@Injectable({
   providedIn: 'root'
})
export class ProductResolver implements Resolve<any> {
  constructor(private productService:ProductService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<any> {
    const id = route.params['id'];
    return this.productService.getProductDetailsById(id);
  }
}