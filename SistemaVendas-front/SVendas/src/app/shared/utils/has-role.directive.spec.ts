import { HasRoleDirective } from "./has-role.directive";

describe('HasRoleDirective', () => {
  it('should create an instance', () => {
    const directive = new HasRoleDirective(null, null ,null);
    expect(directive).toBeTruthy();
  });
});
