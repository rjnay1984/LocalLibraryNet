import { Component, OnInit } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
})
export class NavigationComponent implements OnInit {
  links = ['Login', 'Register'];
  activeLink = this.links[0];
  background: ThemePalette = undefined;

  constructor(public accountService: AccountService) {}

  ngOnInit(): void {}

  logout() {
    this.accountService.logout();
  }
}
