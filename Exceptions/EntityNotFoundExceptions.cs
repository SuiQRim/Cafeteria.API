namespace ProfitTest_Cafeteria.API.Exceptions
{
	public class EntityNotFoundExceptions : Exception
	{
        public EntityNotFoundExceptions()
        {
            
        }
        public EntityNotFoundExceptions(Type type, string id) :
            base($"Entity ({type.Name}) with Id {id} is Not Found")
        { 
        }
    }
}
