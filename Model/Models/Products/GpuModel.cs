namespace Model.Models.Products;
    
public class GpuModel : IProduct
{
    public string id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Price { get; set; }
    public string Team { get; set; } = string.Empty;
    public string GpuProcessorLine { get; set; } = string.Empty;
    public string PCIeCategory { get; set; } = string.Empty;
    public string MemorySize { get; set; } = string.Empty;
    public string MemoryCategory { get; set; } = string.Empty;
    public string MemoryBus { get; set; } = string.Empty;
    public string MemoryRatio { get; set; } = string.Empty;
    public string CoreRatio { get; set; } = string.Empty;
    public string CoresNumber { get; set; } = string.Empty;
    public string CoolingCategory { get; set; } = string.Empty;
    public string OutputsCategory { get; set; } = string.Empty;
    public string SupportedLibraries { get; set; } = string.Empty;
    public string PowerConnector { get; set; } = string.Empty;
    public string RecommendedPSUPower { get; set; } = string.Empty;
    public string Length { get; set; } = string.Empty;
    public string Width { get; set; } = string.Empty;
    public string Height { get; set; } = string.Empty;
    public string Warranty { get; set; } = string.Empty;
    public string ProducentCode { get; set; } = string.Empty;
    public string PgpCode { get; set; } = string.Empty;
    public string GpuProcessorName { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string ProductImg { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}