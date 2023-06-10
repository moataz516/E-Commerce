import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Cart } from 'src/app/model/cart';
import { Product } from 'src/app/model/porduct.model';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit{
  product!:Product
  myCart:Cart
  qty:number = 1
  constructor(private route:ActivatedRoute , private cartService:CartService){}
  ngOnInit(): void {
    this.product = this.route.snapshot.data['data']
  } 

  AddToCart(){
    this.myCart = {
      productId:this.product.productId,
      name:this.product.name,
      price:this.product.price,
      qty:this.qty
    }

    this.cartService.addToCart(this.myCart)
  }
}
