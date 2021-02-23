import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Observable } from 'rxjs';

import { AccountService } from './account.service';

describe('AccountService', () => {
  let service: AccountService;
  const userModel = {
    username: 'user',
    password: 'password',
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(AccountService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('loginAction', () => {
    it('should call login with a username/password', () => {
      const loginSpy = jest.spyOn(service, 'login');
      const login = service.login(userModel);

      expect(login).toBeInstanceOf(Observable);
      expect(loginSpy).toHaveBeenCalledWith(userModel);
    });
  });

  describe('registerAction', () => {
    it('should call register with a username/password', () => {
      const registerSpy = jest.spyOn(service, 'register');
      const register = service.register(userModel);

      expect(register).toBeInstanceOf(Observable);
      expect(registerSpy).toHaveBeenCalledWith(userModel);
    });
  });

  describe('setCurrentUser', () => {
    it('should set the current user on login', () => {
      const user = {
        token:
          'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImRlbW91c2VyQG1pY3Jvc29mdC5jb20iLCJyb2xlIjoiTWVtYmVycyIsIm5iZiI6MTYxMzk2NjAzMywiZXhwIjoxNjE0NTcwODMzLCJpYXQiOjE2MTM5NjYwMzN9.eT32LoOdwK2Eelk64mbUdRaw69156Yihwpv_8yLCA7o',
        username: 'user',
        roles: ['role1'],
      };
      const setCurrentUserSpy = jest.spyOn(service, 'setCurrentUser');
      const getDecodedTokenSpy = jest.spyOn(service, 'getDecodedToken');

      service.login(userModel);
      service.setCurrentUser(user);

      expect(getDecodedTokenSpy).toHaveBeenCalledWith(user.token);
      expect(setCurrentUserSpy).toHaveBeenCalledWith(user);
    });
  });
});
