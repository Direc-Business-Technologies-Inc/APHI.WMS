namespace Integration.SAP.Entities.Transactional.Delivery;

public class DeliveryPayload
{

    public List<DeliveryLinesPayload> DocumentLines { get; private set; } = [];
}
