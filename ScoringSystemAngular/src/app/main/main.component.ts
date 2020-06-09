import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  userId: number;
  userRole: string;
  userRoles = new Array<string>();

  constructor(
    private userService: UserService,
    public translate: TranslateService) { }

  ngOnInit(): void {
    if (localStorage.getItem('token')) {
      var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
      this.userRoles.push("Admin");
      if (this.userService.roleMatch(this.userRoles)) {
        this.userRole = "Admin";
      }
      this.userRoles[0] = "Manager";
      if (this.userService.roleMatch(this.userRoles)) {
        this.userRole = "Manager";
      }
    }
  }

}
