package com.thinktecture.serverless.functions;

import java.time.*;
import com.microsoft.azure.functions.annotation.*;
import com.microsoft.azure.functions.*;

public class KeepItWarm {
    @FunctionName("KeepItWarm")
    public void run(
        @TimerTrigger(name = "timerInfo", schedule = "0 */9 * * * *")
        String timerInfo,
        
        final ExecutionContext context
    ) {
        context.getLogger().info("Java Timer trigger function executed at: " + LocalDateTime.now());
    }
}
