it('loads register page', () => {
  cy.visit('/register');
  cy.contains('Register');
});
