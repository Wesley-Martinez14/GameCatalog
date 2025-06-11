namespace GameCatalog.ViewModels
{
    public class EditRolePermissionsViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public List<PermissionCheckbox> Permissions { get; set; }
    }

    public class PermissionCheckbox
    {
        public int PermissionId { get; set; }
        public string NombrePermiso { get; set; }
        public bool IsSelected { get; set; }
    }

}
