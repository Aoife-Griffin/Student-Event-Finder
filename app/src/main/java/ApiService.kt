import com.example.studenteventfinder.Event
import retrofit2.Call
import retrofit2.http.GET

interface ApiService {

    @GET("api/events")
    fun getEvents(): Call<List<Event>>
}