package com.example.studenteventfinder

import android.os.Bundle
import android.util.Log
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.runtime.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.CardDefaults
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.ui.Alignment
import androidx.compose.material3.Text
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.Card
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview

class MainActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContent {
            EventScreen()
        }
    }
}

@Composable
fun LoadingScreen() {
    Box(
        modifier = Modifier.fillMaxSize(),
        contentAlignment = Alignment.Center
    ) {
        Column(horizontalAlignment = Alignment.CenterHorizontally) {
            CircularProgressIndicator()
            Spacer(modifier = Modifier.height(16.dp))
            Text(stringResource(R.string.loading_events))
        }
    }
}

@Composable
fun EventScreen() {
    var events by remember { mutableStateOf<List<Event>>(emptyList()) }
    var loading by remember { mutableStateOf(true) }
    var error by remember { mutableStateOf<String?>(null) }

    LaunchedEffect(Unit) {
        RetrofitClient.instance.getEvents().enqueue(object : Callback<List<Event>> {

            override fun onResponse(
                call: Call<List<Event>>,
                response: Response<List<Event>>
            ) {
                if (response.isSuccessful) {
                    events = response.body()?.map { event ->
                        Event(
                            eventId = event.eventId,
                            title = event.title,
                            description = event.description,
                            category = event.category,
                            date = event.date,
                            time = event.time,
                            location = event.location,
                            imageUrl = event.imageUrl,
                            organizer = event.organizer
                        )
                    } ?: emptyList()
                    loading = false
                } else {
                    Log.e("API_ERROR", "Response not successful")
                    loading = false
                }
            }

            override fun onFailure(call: Call<List<Event>>, t: Throwable) {
                Log.e("API_FAILURE", "Error: ", t)
                error = t.message ?: "Unknown error"
                loading = false
            }
        })
    }

    when {
        loading -> LoadingScreen()
        error != null -> Text(
            stringResource(R.string.error_message, error!!)
        )
        events.isEmpty() -> Text(stringResource(R.string.no_events))
        else -> EventList(events)
    }
}

@Composable
fun EventList(events: List<Event>) {
    LazyColumn {
        items(events) { event ->
            EventItem(event)
        }
    }
}

@Composable
fun EventItem(event: Event) {
    Card(
        modifier = Modifier
            .fillMaxWidth()
            .padding(horizontal = 12.dp, vertical = 6.dp),
        elevation = CardDefaults.cardElevation(defaultElevation = 6.dp)
    ) {
        Column(modifier = Modifier.padding(16.dp)) {

            // Title
            Text(
                text = event.title,
                style = MaterialTheme.typography.titleMedium,
                fontWeight = FontWeight.Bold
            )

            Spacer(modifier = Modifier.height(8.dp))

            // Description
            Text(
                text = event.description,
                style = MaterialTheme.typography.bodyMedium
            )

            // Location
            Text(
                text = event.location,
                style = MaterialTheme.typography.bodyMedium
            )

            // Date and Time
            Text(
                text = "${event.date} - ${event.time}",
                style = MaterialTheme.typography.bodySmall
            )
        }
    }
}

// Previews

@Preview
@Composable
fun PreviewLoadingScreen() {
    LoadingScreen()
}

@Preview
@Composable
fun PreviewEventScreen() {
    EventScreen()
}

@Preview(locale = "es")
@Composable
fun PreviewEventItemSpanish() {
    EventItem(
        event = Event(
            eventId = 1,
            title = "Evento de Muestra",
            description = "Descripción de evento de muestra",
            category = "Música",
            date = "2026-05-06",
            time = "14:00",
            location = "Lugar de muestra",
            imageUrl = "https://example.com/image.jpg",
            organizer = "Juan Pérez"
        )
    )
}

@Preview(locale = "en")
@Composable
fun PreviewEventItemEnglish() {
    EventItem(
        event = Event(
            eventId = 2,
            title = "Sample Event",
            description = "Sample event description",
            category = "Workshop",
            date = "2026-05-07",
            time = "15:30",
            location = "Sample Location",
            imageUrl = "https://example.com/image.jpg",
            organizer = "Jane Doe"
        )
    )
}