import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ScoringSystemAngular';
  userId: number;

  constructor(
    private router: Router,
    public translate: TranslateService){
      translate.addLangs(['en', 'ua']);
      translate.setDefaultLang('en');
      const browserLang = translate.getBrowserLang();
      translate.use(browserLang.match(/en|ua/) ? browserLang : 'en');
  }

  onChange(se : any){
    console.log(se);
  }

  ngOnInit(): void {
    if (localStorage.getItem('token')) {
      var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
      this.userId = payLoad.userId;
    }
  }

  onLogOut(){
    localStorage.removeItem('token');
    this.userId = null;
    this.router.navigate(['/main']);
  }
}
