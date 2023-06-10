import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Product } from 'src/app/model/porduct.model';
import { ProductService } from 'src/app/service/product.service';

@Component({
  selector: 'app-product-action',
  templateUrl: './product-action.component.html',
  styleUrls: ['./product-action.component.css']
})
export class ProductActionComponent implements OnInit{
products:Product[]
  constructor(private productService:ProductService , private toastr:ToastrService){}
  ngOnInit(): void {
    this.productService.getProductList().subscribe(data=>{
      this.products = data
  })

  this.productService.productChanges.subscribe(d=>{
    this.products = d
  })

  
}

DeleteProduct(id:string){
  this.productService.deleteProductById(id)
}


}
