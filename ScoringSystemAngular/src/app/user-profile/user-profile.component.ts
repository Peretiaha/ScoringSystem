import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/services/user.service';
import { User } from 'src/models/User';
import { JwPaginationComponent } from 'jw-angular-pagination';
import { UsersHealth } from 'src/models/UsersHealth';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  items = [];
  active = 1;
  page = 1;
  user = new User();
  pageOfHealth: Array<UsersHealth>;

  constructor(
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.userService.getUserProfile().subscribe(x=> {
      this.user = x;      
      this.pageOfHealth = this.user.usersHealth;
    });
    this.items = Array(150).fill(0).map((x, i) => ({ id: (i + 1), name: `Item ${i + 1}`}));
  }

  Print() {
    console.log(this.page)
  }

  onChangePage(pageOfHealth: Array<UsersHealth>) {
    // update current page of items
    this.pageOfHealth = pageOfHealth;
}

}
