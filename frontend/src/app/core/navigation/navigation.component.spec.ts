import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/shared/material.module';

import { User } from '../models/user';
import { NavigationComponent } from './navigation.component';

describe('NavigationComponent', () => {
  let component: NavigationComponent;
  let fixture: ComponentFixture<NavigationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule, RouterTestingModule, MaterialModule],
      declarations: [NavigationComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show login/register when no currentUser$', () => {
    expect(fixture).toMatchSnapshot();
  });

  it('should show email when currentUser$', () => {
    const user = {
      token:
        // eslint-disable-next-line max-len
        'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImRlbW91c2VyQG1pY3Jvc29mdC5jb20iLCJyb2xlIjoiTWVtYmVycyIsIm5iZiI6MTYxMzk2NjAzMywiZXhwIjoxNjE0NTcwODMzLCJpYXQiOjE2MTM5NjYwMzN9.eT32LoOdwK2Eelk64mbUdRaw69156Yihwpv_8yLCA7o',
      username: 'user',
      roles: ['role1'],
    };

    component.accountService.setCurrentUser(user);
    let currentUser: User;
    fixture.detectChanges();

    component.accountService.currentUser$.subscribe((response) => {
      currentUser = response;
    });

    expect(currentUser.username).toEqual('demouser@microsoft.com');
    expect(fixture).toMatchSnapshot();
  });
});
