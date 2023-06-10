import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../service/authentication.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {
  constructor(private auth:AuthenticationService) {
  }

  ngOnInit(): void {
  }

  isAuthenticated():Boolean{
    return this.auth.isLoggedIn()
  }
  
}
