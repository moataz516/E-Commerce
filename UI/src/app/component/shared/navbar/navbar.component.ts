import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  userName:string
  cartNumber!:number
  constructor( private cartService:CartService,private auth:AuthenticationService){}

  ngOnInit(): void {
   this.cartNumber = this.cartService.cartNumber(); 
   this.cartService.cartCount.subscribe(d=>{
    this.cartNumber = d
   })
  }

  public logOut = () => {
    this.auth.Logout()
  }

  adminRole(){
    if(this.auth.isLoggedIn){
      let arr = this.auth.getUserRoles();
      if(arr.indexOf('Admin') >=0 ){
        return true
      }
      return false
    }
  }
  

  userRole(){
    if(this.auth.isLoggedIn){
      let arr = this.auth.getUserRoles();
      if(arr.indexOf('User') >=0 ){
        return true
      }
      return false
    }
  }

  isAuthenticated() {
    if(this.auth.isUserAuthenticated()){
      return true
    }
    return false
  }

}
