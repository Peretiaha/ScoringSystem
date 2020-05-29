import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ScoringSystemAngular';
  
  constructor(private router: Router){

  }

  onLogOut(){
    localStorage.removeItem('token');
    this.router.navigate(['/main']);
  }
}
