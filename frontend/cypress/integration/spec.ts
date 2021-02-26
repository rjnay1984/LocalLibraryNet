it('loads home page', () => {
  cy.visit('/');
  cy.contains('Local Library');
});
