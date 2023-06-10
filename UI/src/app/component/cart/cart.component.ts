import { Component, OnInit } from '@angular/core';
import { Cart } from 'src/app/model/cart';
import { Product } from 'src/app/model/porduct.model';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
cart:Cart[];
total:number;
cartNumber:number
constructor(private cartService:CartService){}
ngOnInit(): void {
  this.cart = this.cartService.getCartItems();
  this.total = this.cartService.getTotal()

  this.cartService.cartChanges.subscribe(d=>{
    this.cart=d
    this.total = this.cartService.getTotal()
  })
}

deleteItem(item:Cart){
  this.cartService.removeFromCart(item)
}

decreaseItem(item:any){
  this.cartService.decreaseItem(item)
}
increaseItem(item:any){
  this.cartService.increaseItem(item)
}
clearCart(){
  this.cartService.clearCart();
  this.cart = this.cartService.getCartItems();
  this.total = 0
}

cartCount(){
  return this.cartService.cartNumber()
}

}
