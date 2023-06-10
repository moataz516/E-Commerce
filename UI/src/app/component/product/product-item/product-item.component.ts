import { Component, Input, OnInit } from '@angular/core';
import { Cart } from 'src/app/model/cart';
import { Product } from 'src/app/model/porduct.model';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent implements OnInit{
@Input('productItem') product: Product;
cart:Cart;

constructor(private cartService:CartService){}
ngOnInit(): void {
}

AddToCart(){
  this.cart = {
    productId : this.product.productId,
    name:this.product.name,
    price : this.product.price,
    qty: 1
  }
  this.cartService.addToCart(this.cart);
}
}
