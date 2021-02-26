it('loads login view', () => {
  cy.visit('/login');
  cy.contains('Log In To Access');
});
