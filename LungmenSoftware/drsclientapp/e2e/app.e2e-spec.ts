import { DrsclientappPage } from './app.po';

describe('drsclientapp App', () => {
  let page: DrsclientappPage;

  beforeEach(() => {
    page = new DrsclientappPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
