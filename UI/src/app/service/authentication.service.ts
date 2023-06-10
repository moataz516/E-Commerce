import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { User } from '../model/user.model';
import { catchError, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { JwtHelperService } from '@auth0/angular-jwt';

const   baseUrl = "https://localhost:7292/api/Auth/"

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private userKey = 'user';
  constructor(
    private http:HttpClient,
    private router: Router,
    private toastr:ToastrService,
    private jwtHelper: JwtHelperService
  ) {}

  public Login(username: string, password: string): void {
    this.http.post( baseUrl + 'signin' , {username, password} ).subscribe((result) => {
      console.log(result)
      if(!result['isAuthenticated']){
        this.toastr.error(result["message"])
      }else{
        localStorage.removeItem('user')
        localStorage.setItem('user' , JSON.stringify({'email':result['email'],'username':result['userName']}));
        localStorage.removeItem('jwt')
        localStorage.setItem("jwt", result['token'])
        this.router.navigate(['/'])
        this.toastr.success("User LoggedIn")
        
      }

    });
  }

  public Register(data:any) {
    this.http.post( baseUrl + 'signup' , data ).subscribe( (result) => {
      this.router.navigate(['/login'])
      this.toastr.success("User have been registered ")
    });
  }

  public Logout() {
    localStorage.removeItem('jwt')
    localStorage.removeItem('user')
    this.router.navigate(['/login']);
  }

  public isLoggedIn(): boolean {
    const user = this.getUser();
    if (user) {
      return user != null && user.length > 1;
    }
    return false;
  }

  public getUser() {
    const userJson = localStorage.getItem('jwt');
    if (userJson) {
      return userJson;
    }

    return null;
  }


  public getToken(): any | null {
    const user = this.getUser();
    if (user) {
      return user;
    }
    return null;
  }

  getUserData(){
    const data = localStorage.getItem('user');
    if (data) {
      const userData = JSON.parse(data);
      return userData;
    }
    return null;
  }

  getUserEmail(){
    const data = this.getUserData();
    if(data)
      return data['email'];
    return null
    }

  public getUserName(){
    const data = this.getUserData();
    if(data){
      return data['userName'];    
    }
    return null
  }

  public getUserId(){
    const token = this.getUser()
    if(token){
      const decode = jwtDecode(token)
          return decode['Uid']
    }
    return null
    
  }

  getUserRole(){
    const token = this.getUser()
    if(token){
    const decode = jwtDecode(token)
    return decode['roles']
    }
    return null
  }
  
  getUserRoles(){
    const user = this.getUser()
    if(user){
      const decode = jwtDecode(user)
       return decode['roles']
    }
    return ''
  }


  
  isUserAuthenticated() {
    const token = this.getUser();
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }
  

}
