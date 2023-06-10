import { Component, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { CategoryList } from 'src/app/model/category.model';
import { Product } from 'src/app/model/porduct.model';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { CategoryService } from 'src/app/service/category.service';
import { ProductService } from 'src/app/service/product.service';
import { roles } from 'src/assets/config/config';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  userRoles:string[]
products:Product[];
categories:CategoryList[]
selectedCategoryId:string = 'All'
formData:FormData;
customOptions: OwlOptions = {
  loop: true,
   mouseDrag: false,
   touchDrag: false,
  pullDrag: false,
  dots: false,
  navSpeed: 1200,
  navText: ['<', '>'],
   responsive: {
     0: {
       items: 1
     },
     650: {
       items: 2
     },
     950: {
       items: 3
     },
     1200: {
       items: 4
     },

   },
  nav: true
}
  constructor(private productService:ProductService, private categoryService:CategoryService , private auth:AuthenticationService){}
  ngOnInit(): void {
    this.categoryService.getCategoryList().subscribe(d=>{
      this.categories = d
    })
    this.productService.getProductList().subscribe(data=>{
      this.products = data;
    })

  }

  selectedCategory(id:string){
    this.selectedCategoryId = id;
  }




}
